import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Button } from "./components/ui/button";
import { Eye, Pen, Trash } from "lucide-react";
import { Dialog } from "@radix-ui/react-dialog";

import { DeleteProduct, DialogEdit, DialogView } from "./components/web-app";

const invoices = [
  {
    user: "INV001",
    paymentStatus: "Paid",
    paymentMethod: "Credit Card",
  },
  {
    user: "INV002",
    paymentStatus: "Pending",
    paymentMethod: "PayPal",
  },
  {
    user: "INV003",
    paymentStatus: "Unpaid",
    paymentMethod: "Bank Transfer",
  },
  {
    user: "INV004",
    paymentStatus: "Paid",
    paymentMethod: "Credit Card",
  },
  {
    user: "INV005",
    paymentStatus: "Paid",
    paymentMethod: "PayPal",
  },
  {
    user: "INV006",
    paymentStatus: "Pending",
    paymentMethod: "Bank Transfer",
  },
  {
    user: "INV007",
    paymentStatus: "Unpaid",
    paymentMethod: "Credit Card",
  },
];

function App() {
  return (
    <main className="w-screen h-screen grid place-items-center">
      <section className="w-1/2">
        <div className="w-full flex justify-end">
          <Button>Create</Button>
        </div>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead className="w-[100px]">User</TableHead>
              <TableHead>Status</TableHead>
              <TableHead>Method</TableHead>
              <TableHead className="text-right">Actions</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {invoices.map((invoice) => (
              <TableRow key={invoice.user}>
                <TableCell className="font-medium">{invoice.user}</TableCell>
                <TableCell>{invoice.paymentStatus}</TableCell>
                <TableCell>{invoice.paymentMethod}</TableCell>
                <TableCell className="flex justify-end gap-4">
                  <DialogView />

                  <DialogEdit />

                  <DeleteProduct />
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </section>
    </main>
  );
}

export { App };
