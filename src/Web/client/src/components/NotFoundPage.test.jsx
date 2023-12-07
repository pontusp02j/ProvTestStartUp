import React from 'react';
import { render, fireEvent, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import { useNavigate } from 'react-router-dom';
import NotFoundPage from './NotFoundPage';

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useNavigate: jest.fn(),
}));

describe('NotFoundPage', () => {
  it('should render correctly and navigate on button click', () => {
    const mockNavigate = jest.fn();
    useNavigate.mockReturnValue(mockNavigate);

    render(<NotFoundPage />);

    expect(screen.getByText("Sorry, the page you visited does not exist.")).toBeInTheDocument();

    fireEvent.click(screen.getByText("Click here / Product Page"));

    expect(mockNavigate).toHaveBeenCalledWith('/products');
  });
});
