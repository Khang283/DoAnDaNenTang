const express=require('express');
const routes=express.Router();
const CartController=require('../controllers/CartController');

routes.post('/',CartController.GetList);
routes.post('/add',CartController.add);
routes.delete('/delete',CartController.delete);
routes.put('/inc',CartController.inc);
routes.put('/reduce',CartController.reduce);

module.exports=routes;
module.exports=routes;