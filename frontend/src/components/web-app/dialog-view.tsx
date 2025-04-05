import { useState, useEffect } from "react";
import { Eye } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { getOrderById } from "@/service/getOrder"; 

interface DialogViewProps {
  orderId: string;
}

export function DialogView({ orderId }: DialogViewProps) {
  const [orderDetails, setOrderDetails] = useState<any>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string>("");

  useEffect(() => {
    const fetchOrderDetails = async () => {
      setLoading(true);
      try {
        const data = await getOrderById(orderId); 
        setOrderDetails(data); 
      } catch (error) {
        setError("Erro ao carregar os detalhes do pedido");
      } finally {
        setLoading(false);
      }
    };

    if (orderId) {
      fetchOrderDetails();
    }
  }, [orderId]);

  if (loading) return <p>Carregando...</p>;
  if (error) return <p>{error}</p>;

  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button size="icon" className="rounded-full">
          <Eye />
        </Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Detalhes do Pedido</DialogTitle>
          <DialogDescription>
            Veja as informações detalhadas do pedido.
          </DialogDescription>
        </DialogHeader>
        <div className="grid gap-4 py-4">
          {orderDetails && (
            <>
              <div className="grid grid-cols-4 items-center gap-4">
                <Label htmlFor="cliente" className="text-right">
                  Cliente
                </Label>
                <Input
                  id="cliente"
                  value={orderDetails.cliente}
                  className="col-span-3"
                  disabled
                />
              </div>
              <div className="grid grid-cols-4 items-center gap-4">
                <Label htmlFor="produto" className="text-right">
                  Produto
                </Label>
                <Input
                  id="produto"
                  value={orderDetails.produto}
                  className="col-span-3"
                  disabled
                />
              </div>
              <div className="grid grid-cols-4 items-center gap-4">
                <Label htmlFor="valor" className="text-right">
                  Valor
                </Label>
                <Input
                  id="valor"
                  value={orderDetails.valor}
                  className="col-span-3"
                  disabled
                />
              </div>
              <div className="grid grid-cols-4 items-center gap-4">
                <Label htmlFor="status" className="text-right">
                  Status
                </Label>
                <Input
                  id="status"
                  value={orderDetails.status}
                  className="col-span-3"
                  disabled
                />
              </div>
              <div className="grid grid-cols-4 items-center gap-4">
                <Label htmlFor="dataCriacao" className="text-right">
                  Data Criação
                </Label>
                <Input
                  id="dataCriacao"
                  value={new Date(orderDetails.dataCriacao).toLocaleString()}
                  className="col-span-3"
                  disabled
                />
              </div>
            </>
          )}
        </div>
      </DialogContent>
    </Dialog>
  );
}