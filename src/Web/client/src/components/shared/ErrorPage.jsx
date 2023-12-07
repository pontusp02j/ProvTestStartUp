// ErrorPage.jsx
import React from 'react';
import { Result, Button } from 'antd';
import { useNavigate } from 'react-router-dom';

const ErrorPage = ({ error }) => {
  const navigate = useNavigate();

  const goBack = () => {
    navigate(-1);
  };

  return (
    <Result
      status="error"
      title="There's a Problem"
      subTitle={error.message || 'An unknown error occurred.'}
      extra={
        <Button type="primary" onClick={goBack}>
          Go Back
        </Button>
      }
    />
  );
};

export default ErrorPage;
