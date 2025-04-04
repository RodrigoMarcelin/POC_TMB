import api from './api';

export const getOrderById = async (id: string) => {
    try {
      const response = await api.get(`/orders/${id}`);
      if (response.data.success) {
        return response.data.data; 
      } else {
        throw new Error('Falha ao carregar os detalhes do pedido');
      }
    } catch (error) {
      console.error('Erro ao buscar os detalhes do pedido:', error);
      throw error;
    }
  };