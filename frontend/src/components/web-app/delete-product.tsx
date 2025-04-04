import { Trash } from "lucide-react";
import React, { useState } from "react";
import { Button } from "@/components/ui/button";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger,
} from "@/components/ui/alert-dialog";
import { deleteOrder } from "@/service/deleteOrder"; 

interface DeleteProductProps {
  orderId: string;
  onDelete: () => void; 
}

export function DeleteProduct({ orderId, onDelete }: DeleteProductProps) {
  const [open, setOpen] = useState(false);

  const handleDelete = async () => {
    try {
      const result = await deleteOrder(orderId);
      if (result) {
        onDelete(); 
        alert("Pedido excluído com sucesso!");
      } else {
        alert("Erro ao excluir pedido");
      }
      setOpen(false);
    } catch (error) {
      console.error("Erro ao excluir o pedido:", error);
      alert("Houve um erro ao tentar excluir o pedido.");
      setOpen(false); 
    }
  };

  return (
    <AlertDialog open={open} onOpenChange={setOpen}>
      <AlertDialogTrigger asChild>
        <Button size="icon" className="rounded-full" variant="destructive">
          <Trash />
        </Button>
      </AlertDialogTrigger>

      <AlertDialogContent>
        <AlertDialogHeader>
          <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
          <AlertDialogDescription>
            This action cannot be undone. This will permanently delete your
            account and remove your data from our servers.
          </AlertDialogDescription>
        </AlertDialogHeader>

        <AlertDialogFooter>
          <AlertDialogCancel onClick={() => setOpen(false)}>Cancel</AlertDialogCancel>
          <AlertDialogAction onClick={handleDelete}>Continue</AlertDialogAction>
        </AlertDialogFooter>
      </AlertDialogContent>
    </AlertDialog>
  );
}