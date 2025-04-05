import { useState } from "react";
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
import { createOrder } from "@/service/createOrder";

export function DialogCreate() {
  const [open, setOpen] = useState(false);
  const [formData, setFormData] = useState({
    cliente: "",
    produto: "",
    valor: "",
  });


  const handleCreate = async () => {
    if (!formData.cliente || !formData.produto || !formData.valor) {
      alert("Todos os campos devem ser preenchidos corretamente!");
      return;
    }

    if (isNaN(Number(formData.valor))) {
      alert("Por favor, insira um valor válido.");
      return;
    }

    try {
      const newOrder = {
        cliente: formData.cliente,
        produto: formData.produto,
        valor: parseFloat(formData.valor), 
      };
      await createOrder(newOrder); 
      // alert("Pedido criado com sucesso!");
      setOpen(false); 
    } catch (error) {
      console.error("Erro ao criar o pedido", error);
      alert("Erro ao criar o pedido.");
    }
  };

  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button>Criar pedido</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Criar Pedido</DialogTitle>
          <DialogDescription>
            Preencha os campos abaixo para criar um novo pedido.
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
              onChange={(e) =>
                setFormData({ ...formData, cliente: e.target.value })
              }
              className="col-span-3"
            />
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="produto" className="text-right">
              Produto
            </Label>
            <Input
              id="produto"
              value={formData.produto}
              onChange={(e) =>
                setFormData({ ...formData, produto: e.target.value })
              }
              className="col-span-3"
            />
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="valor" className="text-right">
              Valor
            </Label>
            <Input
              id="valor"
              value={formData.valor}
              onChange={(e) =>
                setFormData({ ...formData, valor: e.target.value })
              }
              className="col-span-3"
            />
          </div>
        </div>

        <div className="flex justify-end gap-4">
          <Button variant="outline" onClick={() => setOpen(false)}>
            Cancelar
          </Button>
          <Button onClick={handleCreate}>Salvar</Button>
        </div>
      </DialogContent>
    </Dialog>
  );
}