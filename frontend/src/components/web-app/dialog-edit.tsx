import { Pencil } from "lucide-react";
import { useState, useEffect } from "react";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { updateOrder } from "@/service/updateOrder";
import { getOrderById } from "@/service/getOrder";

export function DialogEdit({ orderId }: { orderId: string }) {
  const [open, setOpen] = useState(false);
  const [formData, setFormData] = useState({
    cliente: "",
    produto: "",
    valor: "",
    status: "",
    dataCriacao: "",
  });

  const [errors, setErrors] = useState({
    cliente: "",
    produto: "",
    valor: "",
    status: "",
    dataCriacao: "",
  });

  const [updateSuccess, setUpdateSuccess] = useState(false); // Para indicar se a atualização foi bem-sucedida
  const [updateMessage, setUpdateMessage] = useState(""); // Para exibir a mensagem de sucesso ou erro

  useEffect(() => {
    const fetchOrder = async () => {
      const order = await getOrderById(orderId);
      if (order) {
        setFormData({
          cliente: order.cliente || "",
          produto: order.produto || "",
          valor: order.valor.toString() || "",
          status: order.status || "",
          dataCriacao: order.dataCriacao || "", // Mantendo a dataCriacao no estado
        });
      }
    };

    if (orderId) {
      fetchOrder();
    }
  }, [orderId]);

  const handleUpdate = async () => {
    if (!formData.cliente || !formData.produto || !formData.valor) {
      setUpdateMessage("Todos os campos devem ser preenchidos corretamente!");
      setUpdateSuccess(false);
      return;
    }

    if (isNaN(Number(formData.valor))) {
      setUpdateMessage("Por favor, insira um valor válido.");
      setUpdateSuccess(false);
      return;
    }

    // Envia os dados atualizados, incluindo a dataCriacao
    try {
      const updatedOrder = {
        id: orderId,
        cliente: formData.cliente,
        produto: formData.produto,
        valor: parseFloat(formData.valor),
        status: formData.status || "", // Garantindo que o status seja enviado
        dataCriacao: formData.dataCriacao || "", // Garantindo que a dataCriacao seja mantida
      };

      await updateOrder(orderId, updatedOrder);
      setUpdateMessage("Pedido atualizado com sucesso!");
      setUpdateSuccess(true);
      setOpen(false); // Fechar o diálogo de edição após o sucesso
    } catch (error) {
      console.error("Erro ao atualizar o pedido", error);
      setUpdateMessage("Erro ao atualizar o pedido.");
      setUpdateSuccess(false);
    }
  };

  const validateField = (field: string, value: string) => {
    if (field === "valor") {
      if (isNaN(Number(value))) {
        setErrors((prev) => ({
          ...prev,
          valor: "Por favor, insira um valor válido.",
        }));
      } else {
        setErrors((prev) => ({ ...prev, valor: "" }));
      }
    } else {
      if (!value) {
        setErrors((prev) => ({ ...prev, [field]: "Este campo é obrigatório." }));
      } else {
        setErrors((prev) => ({ ...prev, [field]: "" }));
      }
    }
  };

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button size="icon" className="rounded-full" variant="outline">
          <Pencil />
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Editar Pedido</DialogTitle>
          <DialogDescription>
            Atualize os campos abaixo para modificar o pedido.
          </DialogDescription>
        </DialogHeader>

        <div className="grid gap-4 py-4">
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="cliente" className="text-right">
              Cliente
            </Label>
            <Input
              id="cliente"
              value={formData.cliente}
              onChange={(e) => {
                const value = e.target.value;
                setFormData({ ...formData, cliente: value });
                validateField("cliente", value);
              }}
              className="col-span-3"
            />
            {errors.cliente && <span className="text-red-500">{errors.cliente}</span>}
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="produto" className="text-right">
              Produto
            </Label>
            <Input
              id="produto"
              value={formData.produto}
              onChange={(e) => {
                const value = e.target.value;
                setFormData({ ...formData, produto: value });
                validateField("produto", value);
              }}
              className="col-span-3"
            />
            {errors.produto && <span className="text-red-500">{errors.produto}</span>}
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="valor" className="text-right">
              Valor
            </Label>
            <Input
              id="valor"
              value={formData.valor}
              onChange={(e) => {
                const value = e.target.value;
                setFormData({ ...formData, valor: value });
                validateField("valor", value);
              }}
              className="col-span-3"
            />
            {errors.valor && <span className="text-red-500">{errors.valor}</span>}
          </div>
        </div>

        {/* Exibindo a mensagem de sucesso ou erro */}
        <div className="my-4">
          {updateMessage && (
            <p className={`text-center ${updateSuccess ? "text-green-500" : "text-red-500"}`}>
              {updateMessage}
            </p>
          )}
        </div>

        <div className="flex justify-end gap-4">
          <Button variant="outline" onClick={() => setOpen(false)}>
            Cancelar
          </Button>
          <Button
            onClick={handleUpdate}
            disabled={!!errors.cliente || !!errors.produto || !!errors.valor}
          >
            Salvar
          </Button>
        </div>
      </DialogContent>
    </Dialog>
  );
}