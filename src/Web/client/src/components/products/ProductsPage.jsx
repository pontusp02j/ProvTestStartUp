import React, { useState, useEffect, useCallback } from 'react';
import { Table, InputNumber, Select, Pagination, Space } from 'antd';
import { useNavigate } from 'react-router-dom';
import { debounce } from 'lodash';
import productTableColumns from './productTableColumns';
import MobileProductView from './MobileProductView';
import { api } from './../../shared/requests/api';
import useWindowSize from './../shared/useWindowSize';

const { Option } = Select;

const ProductsPage = () => {
  const [products, setProducts] = useState([]);
  const [categories, setCategories] = useState([]);
  const [loading, setLoading] = useState(false);
  const [pagination, setPagination] = useState({ currentPage: 1, pageSize: 8, total: 0 });
  const [filters, setFilters] = useState({ minPrice: null, maxPrice: null, category: '' });
  const navigate = useNavigate();
  const size = useWindowSize();
  const isMobile = size.width <= 768;

  const fetchProducts = useCallback(async () => {
    setLoading(true);
    const params = new URLSearchParams({
      pageNumber: pagination.currentPage,
      pageSize: pagination.pageSize,
      minPrice: filters.minPrice || 0,
      maxPrice: filters.maxPrice || 10000000,
      category: filters.category || ''
    });

    try {
      const response = await api.read(`products?${params.toString()}`);
      setProducts(response.products);
      setPagination(prev => ({ ...prev, total: response.totalCount }));
      if (!categories.length) {
        setCategories(response.categories);
      }
    } catch (error) {
      navigate('/error', { state: { error } });
    } finally {
      setLoading(false);
    }
  }, [pagination.currentPage, pagination.pageSize, filters, navigate]);

  useEffect(() => {
    fetchProducts();
  }, [pagination.currentPage, pagination.pageSize, filters.minPrice, filters.maxPrice, filters.category, fetchProducts]);

  const handleFilterChange = debounce((filterType, value) => {
    setFilters({ ...filters, [filterType]: value });
    setPagination({ ...pagination, currentPage: 1 });
  }, 300);

  const handleTableChange = (page, pageSize) => {
    setPagination({ ...pagination, currentPage: page, pageSize });
  };

  return (
    <div>
      <Space style={{ marginBottom: 16, width: '100%' }} direction="vertical">
        <InputNumber
          placeholder="Min Price"
          onChange={(value) => handleFilterChange('minPrice', value)}
          style={{ width: '100%' }}
        />
        <InputNumber
          placeholder="Max Price"
          onChange={(value) => handleFilterChange('maxPrice', value)}
          style={{ width: '100%' }}
        />
        <Select 
          placeholder="Select Category"
          onChange={(value) => handleFilterChange('category', value)}
          style={{ width: '100%' }}>
          <Option value="">All Categories</Option>
          {categories.map(category => (
            <Option key={category} value={category}>{category}</Option>
          ))}
        </Select>
      </Space>

      {!isMobile ? (
        <Table
          columns={productTableColumns}
          dataSource={products}
          rowKey="id"
          loading={loading}
          pagination={false}
          responsive
        />
      ) : (
        products.map(product => <MobileProductView key={product.id} product={product} />)
      )}

      <Pagination
        current={pagination.currentPage}
        pageSize={pagination.pageSize}
        total={pagination.total}
        onChange={handleTableChange}
        showTotal={(total, range) => `${range[0]}-${range[1]} of ${total} items`}
        style={{ marginTop: 16, textAlign: 'center' }}
      />
    </div>
  );
};

export default ProductsPage;
