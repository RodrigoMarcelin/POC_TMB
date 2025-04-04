import api from './api';

export const getOrders = async () => {
    try {
      const response = await api.get('/orders'); 
      if (response.data.success) {
        return response.data.data;
      } else {
        throw new Error('Falha ao carregar pedidos');
      }
    } catch (error) {
      console.error('Erro na requisição:', error);
      throw error; 
    }
  };