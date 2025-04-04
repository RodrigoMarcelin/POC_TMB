import api from './api';  

export const deleteOrder = async (orderId: string) => {
  try {
    const response = await api.delete(`/orders/${orderId}`);
    if (response.status === 200) {
      return true;  
    } else {
      return false;
    }
  } catch (error) {
    console.error('Erro ao excluir o pedido:', error);
    return false; 
  }
};