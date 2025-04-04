import api from './api';

export const updateOrder = async (orderId: string, updatedData: any) => {
    try {
      const response = await api.put(`/orders/${orderId}`, updatedData);
      return response.data;
    } catch (error) {
      console.error("Erro ao atualizar o pedido:", error);
      throw new Error("Erro ao atualizar o pedido");
    }
  };