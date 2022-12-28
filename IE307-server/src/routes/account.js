const express = require('express');
const routes=express.Router();
const AccountController=require('../controllers/AccountController');

routes.put('/update',AccountController.update);
routes.post('/login', AccountController.login);
routes.post('/register', AccountController.register);

module.exports=routes;