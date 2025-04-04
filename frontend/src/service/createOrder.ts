import api from './api';

export const createOrder = async (orderData: { cliente: string; produto: string; valor: number }) => {
  try {
    const response = await api.post('/orders', orderData);
    return response.data;
  } catch (error) {
    console.error("Erro ao criar o pedido:", error);
    throw new Error("Erro ao criar o pedido");
  }
};