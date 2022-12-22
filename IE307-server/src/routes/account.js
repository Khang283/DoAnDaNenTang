const express = require('express');
const routes=express.Router();
const AccountController=require('../controllers/AccountController');

routes.post('/login', AccountController.login);

module.exports=routes;