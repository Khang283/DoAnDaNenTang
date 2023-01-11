const express = require('express');
const app=express();
const bodyParser=require('body-parser');
const morgan=require('morgan');
const dotenv=require('dotenv').config({path: '.env'});
const PORT=5001; 
const db=require('./models/connection');
const routes=require('./routes/index');

//FOR USING JSON
app.use(express.json());
app.use(express.urlencoded({extended: true}));

//BODY PARSER
app.use(bodyParser.json());

//MORGAN
app.use(morgan('combined'));

//CONNECT TO DB
db.connect();

//Routes
routes(app);

process.env.TZ="Asia/Ho_Chi_Minh"

let server=app.listen(process.env.PORT || PORT,()=>{
    console.log(new Date().toLocaleString('en-US',{timeZone: 'Asia/Ho_Chi_Minh'}));
    console.log(`Server start on ${server.address().port}`);
});