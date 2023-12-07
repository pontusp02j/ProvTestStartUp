import React from 'react';
import { render, screen, waitFor } from '@testing-library/react';
import ProductsPage from './ProductsPage';
import { BrowserRouter as Router } from 'react-router-dom';

jest.mock('./../../shared/requests/api', () => ({
  api: {
    read: jest.fn(() => Promise.resolve({ products: [], totalCount: 0, categories: [] })),
  },
}));

window.matchMedia = jest.fn(() => ({
  matches: false,
  addEventListener: jest.fn(),
  removeEventListener: jest.fn(),
}));

describe('ProductsPage', () => {
  it('renders without crashing and shows loading', async () => {
    render(
      <Router>
        <ProductsPage />
      </Router>
    );

    expect(screen.getByText(/loading.../i)).toBeInTheDocument();

    await waitFor(() => {
      expect(screen.queryByText(/loading.../i)).not.toBeInTheDocument();
    });
  });
});
