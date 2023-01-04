const express=require('express');
const routes=express.Router();
const ProductsController=require('../controllers/ProductsController');

routes.get('/bestsale',ProductsController.BestSale);
routes.post('/favorite',ProductsController.GetFavorite);
routes.get('/:category/:page',ProductsController.Category);
routes.put('/removefavorite',ProductsController.RemoveFavorite);
routes.post('/checkfavorite',ProductsController.CheckFavorite);
routes.post('/addToFavorite',ProductsController.AddToFavorite);
routes.get('/',ProductsController.index);

module.exports = routes;