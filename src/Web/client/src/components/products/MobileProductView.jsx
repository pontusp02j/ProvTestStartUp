import React from 'react';
import { Row, Col } from 'antd';
import PropTypes from 'prop-types';

const MobileProductView = ({ product }) => (
  <div style={{ 
    marginBottom: '20px', 
    padding: '10px', 
    boxShadow: '0 4px 8px 0 rgba(0,0,0,0.2)', 
    borderRadius: '5px',
    backgroundColor: '#fff' 
  }}>
    <Row gutter={[16, 16]}>
      <Col span={24} style={{ textAlign: 'left' }}>
        <img src={product.image} alt={product.title} style={{ maxWidth: '25%', height: 'auto', borderRadius: '5px' }} />
      </Col>
      <Col span={24} style={{ marginTop: '10px' }}>
        <strong>ID:</strong> {product.id}
      </Col>
      <Col span={24}>
        <strong>Title:</strong> <a href={`/products/${product.id}`}>{product.title}</a>
      </Col>
      <Col span={24}>
        <strong>Price:</strong> ${product.price.toFixed(2)}
      </Col>
      <Col span={24}>
        <strong>Category:</strong> {product.category}
      </Col>
    </Row>
  </div>
);

MobileProductView.propTypes = {
  product: PropTypes.object.isRequired,
};

export default MobileProductView;
