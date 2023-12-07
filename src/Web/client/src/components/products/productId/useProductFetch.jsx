import { useState, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { api } from '../../../shared/requests/api';

const useProductFetch = (id) => {
  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const fetchProduct = useCallback(async () => {
    setLoading(true);
    try {
      const response = await api.read(`products/${id}`);
      setProduct(response);
    } catch (error) {
      navigate('/error', { state: { error } });
    } finally {
      setLoading(false);
    }
  }, [id, navigate]);

  return { product, loading, fetchProduct };
};

export default useProductFetch;
