import React from 'react';
import { render, fireEvent, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import { useNavigate } from 'react-router-dom';
import ErrorPage from './ErrorPage';

jest.mock('react-router-dom', () => ({
  ...jest.requireActual('react-router-dom'),
  useNavigate: jest.fn(),
}));

describe('ErrorPage', () => {
  it('should render error message and navigate back on button click', () => {
    const mockNavigate = jest.fn();
    useNavigate.mockReturnValue(mockNavigate);

    const error = { message: 'Test Error' };
    render(<ErrorPage error={error} />);

    expect(screen.getByText('Test Error')).toBeInTheDocument();

    fireEvent.click(screen.getByText("Go Back"));

    expect(mockNavigate).toHaveBeenCalledWith(-1);
  });

  it('should render default error message if no error message provided', () => {
    render(<ErrorPage error={{}} />);

    expect(screen.getByText('An unknown error occurred.')).toBeInTheDocument();
  });
});
