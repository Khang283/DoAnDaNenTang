const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const ObjectId=Schema.ObjectId;

const Product = new Schema({
    productId: { type: ObjectId },
    name: { type: String, required: true, max: 255 },
    description: { type: String, max: 800 },
    image: { type: String, max: 255 },
    price: { type: String, default: 0 },
    type: { type: String, max: 25 },
    material: { type: String, max: 25, default: 'Silver' },
    createdAt: { type: Date, default: Date.now },
    updatedAt: { type: Date, default: Date.now },
});

const Account = new Schema({
    userId:{
        type: ObjectId,
    },
    username: {
        type: String,
        required: true,
        unique: true,
        max: 50,
    },
    password: {
        type: String,
        required: true,
        max: 30,
    },
    role:{
        type: String,
        default: "USER",
    },
    email: {
        type: String,
        default: "none@gmail.com",
    },
    phone: {
        type: String,
    },
    address: {
        type: String,
    }, 
    favorite: [{
        item: Product,
    }],

},{
    timestamps: true,
});

module.exports = mongoose.model("Account", Account);