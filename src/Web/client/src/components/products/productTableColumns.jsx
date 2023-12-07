import React from 'react';

const productTableColumns = [
  {
    title: 'ID',
    dataIndex: 'id',
    key: 'id',
    align: 'center',
    responsive: ['md'],
    width: 50,
  },
  {
    title: 'Image',
    dataIndex: 'image',
    key: 'image',
    align: 'center',
    render: image => <img src={image} alt="Product" style={{ maxWidth: '50px', height: 'auto' }} />,
    width: 70,
  },
  {
    title: 'Title',
    dataIndex: 'title',
    key: 'title',
    align: 'left',
    render: (text, record) => (
      <a href={`/products/${record.id}`} style={{ whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' }}>{text}</a>
    ),
    width: 200,
  },
  {
    title: 'Category',
    dataIndex: 'category',
    key: 'category',
    align: 'left',
    responsive: ['sm'],
    width: 100,
  },
  {
    title: 'Price',
    dataIndex: 'price',
    key: 'price',
    align: 'right',
    render: price => `$${price.toFixed(2)}`,
    width: 100,
  },
];

export default productTableColumns;
