const mongoose=require('mongoose');
const dotenv=require('dotenv').config({path: './.env'});

const DATABASE_URL=process.env.DATABASE_URL;
async function connect(){
    try{
        await mongoose.connect(DATABASE_URL,{
            useNewUrlParser: true,
            useUnifiedTopology: true,
        });
        console.log("Connect to database successfully ");
    }
    catch{
        console.log("Connect to database failed");
    }
}

module.exports={connect};