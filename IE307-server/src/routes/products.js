const express=require('express');
const routes=express.Router();
const ProductsController=require('../controllers/ProductsController');

routes.put('/removefavorite',ProductsController.RemoveFavorite);
routes.post('/checkfavorite',ProductsController.CheckFavorite);
routes.post('/addToFavorite',ProductsController.AddToFavorite);
routes.get('/',ProductsController.index);

module.exports = routes;