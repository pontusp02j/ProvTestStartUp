import React from 'react';
import { render, act } from '@testing-library/react';
import useWindowSize from './useWindowSize';


function TestComponent() {
  const { width, height } = useWindowSize();
  return <div data-testid="size">{`${width} x ${height}`}</div>;
}

describe('useWindowSize', () => {
  it('should display the current window size', () => {
    const { getByTestId } = render(<TestComponent />);
    expect(getByTestId('size').textContent).toBe(`${window.innerWidth} x ${window.innerHeight}`);
  });

  it('should update size on window resize', () => {
    const { getByTestId } = render(<TestComponent />);

    act(() => {
      window.innerWidth = 500;
      window.innerHeight = 600;
      window.dispatchEvent(new Event('resize'));
    });

    expect(getByTestId('size').textContent).toBe('500 x 600');
  });
});
