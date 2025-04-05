import { useEffect, useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import * as signalR from "@microsoft/signalr";
import { DeleteProduct, DialogEdit, DialogView, DialogCreate } from "./components/web-app";
import { getOrders } from "./service/orderService";
import logo from "@/assets/logo.png"; 

const App = () => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const data = await getOrders();
        const ordersData = data ?? [];
        setOrders(ordersData);
      } catch (error: any) {
        setError(error.message || "Erro ao carregar os pedidos.");
      } finally {
        setLoading(false);
      }
    };

    fetchOrders();
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5000/orderhub")
      .withAutomaticReconnect()
      .build();

    connection.on("OrderUpdated", () => {
      console.log("🔄 Pedido atualizado via SignalR");
      fetchOrders(); // Atualiza os dados
    });

    connection.start().catch((err) =>
      console.error("Erro ao conectar ao SignalR:", err)
    );

    return () => {
      connection.stop();
    };
  }, []);

  const handleDelete = (deletedOrderId: string) => {
    setOrders(orders.filter((order: any) => order.id !== deletedOrderId)); 
  };

  if (loading) return <p>Carregando...</p>;
  if (error) return <p>{error}</p>;

  return (
    <main className="w-full h-screen flex flex-col items-center justify-center gap-2">
     
      <section className="w-full bg-black text-white p-4 fixed top-0 left-0 right-0 z-10">
        <h1 className="text-2xl font-bold">Order Management</h1>
      </section>

      <div>
        <img src={logo} alt="TMB Logo" className="mx-auto h-32" /> 
      </div>

      <section className="w-full max-w-6xl mt-4">
        <div className="w-full flex justify-end mb-4">
          <DialogCreate />
        </div>

        <div className="overflow-x-auto">
          <Table className="w-full">
            <TableHeader>
              <TableRow>
                <TableHead className="w-[250px] text-left py-2">Produto</TableHead>
                <TableHead className="w-[150px] text-left py-2">Valor</TableHead>
                <TableHead className="w-[150px] text-left py-2">Status</TableHead>
                <TableHead className="text-right px-4 py-2 pr-17">Ações</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {orders.map((order: any) => (
                <TableRow key={order.id}>
                  <TableCell className="text-left py-2 w-1/4">{order.produto}</TableCell>
                  <TableCell className="text-left py-2 w-1/4">R$ {order.valor.toFixed(2)}</TableCell>
                  <TableCell className="text-left py-2 w-1/4">{order.status}</TableCell>
                  <TableCell className="text-right py-2 space-x-4 flex justify-end">
                    <DialogView orderId={order.id} />
                    <DialogEdit orderId={order.id} />
                    <DeleteProduct orderId={order.id} onDelete={() => handleDelete(order.id)} />
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </div>
      </section>
    </main>
  );
};

export { App };