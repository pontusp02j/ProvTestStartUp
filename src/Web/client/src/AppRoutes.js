import SinglePageProduct from "./components/products/productId/SinglePageProduct";
import ProductsPage from "./components/products/ProductsPage";
import NotFoundPage from './components/NotFoundPage'; 
import ErrorPage from './components/shared/ErrorPage';
const AppRoutes = [
  {
    path: '/',
    element: <ProductsPage />
  },
  {
    path: '/products/',
    element: <ProductsPage />
  },
  {
    path: '/products/:id',
    element: <SinglePageProduct />
  },
  {
    path: '/error',
    element: <ErrorPage />
  },
  {
    path: '*',
    element: <NotFoundPage />
  }
];

export default AppRoutes;
