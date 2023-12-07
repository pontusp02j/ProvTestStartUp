import React, { useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Rate, Card, Spin, Button } from 'antd';
import useProductFetch from './useProductFetch';

const SinglePageProduct = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { product, loading, fetchProduct } = useProductFetch(id);

  useEffect(() => {
    fetchProduct();
  }, [fetchProduct]);

  const goBack = () => navigate('/products');

  if (loading) {
    return <div style={{ textAlign: 'center', marginTop: '50px' }}><Spin size="large" /></div>;
  }

  if (!product) {
    return <p style={{ textAlign: 'center' }}>Product not found</p>;
  }

  return (
    <div style={{ display: 'flex', justifyContent: 'center', margin: '10em' }}>
      <Card
        hoverable
        style={{ width: 300, padding: '20px' }}
        cover={<img alt={product.title} src={product.image} style={{ objectFit: 'cover' }} />}
      >
        <Card.Meta title={product.title} description={product.description} />
        <Rate allowHalf disabled value={product.rating.rate} style={{ marginTop: '10px' }} />
        <p>Price: ${product.price.toFixed(2)}</p>
        <Button type="primary" onClick={goBack}>Back to Products</Button>
      </Card>
    </div>
  );
};

export default SinglePageProduct;
