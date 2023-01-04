const express=require('express');
const routes=express.Router();
const CartController=require('../controllers/CartController');

routes.post('/history',CartController.History);
routes.post('/checkout',CartController.Checkout);
routes.post('/',CartController.GetList);
routes.post('/add',CartController.add);
routes.put('/delete',CartController.delete);
routes.put('/inc',CartController.inc);
routes.put('/reduce',CartController.reduce);

module.exports=routes;
