const express=require('express');
const routes=express.Router();
const CartController=require('../controllers/CartController');

routes.post('/',CartController.GetList);
routes.post('/add',CartController.add);

module.exports=routes;