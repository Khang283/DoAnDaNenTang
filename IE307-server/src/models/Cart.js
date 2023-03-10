const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const ObjectId = Schema.ObjectId;
const mongooseDelete=require('mongoose-delete');
const { all } = require('../routes/cart');

const ProductSchema = new Schema({
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

const Cart = new Schema({
    cartID:{
        type: ObjectId,
    },
    userID:{
        type: String,
        required: true
    },
    totalQuantity:{
        type: Number,
        default: 0
    },
    totalPrice: {
        type: Number,
        default: 0
    },
    receiver: {
        type: String,
    },
    phone: {
        type: String
    },
    address: {
        type: String
    },
    pay: {
        type: String
    },
    items:[{
        item: ProductSchema,
        quantity: {
            type: Number,
            default: 0
        },
        price: {
            type: Number,
            default: 0
        }
    }]
},{timestamps: true});

Cart.plugin(mongooseDelete,{overrideMethods: 'all', deletedAt: true});

module.exports=new mongoose.model("Cart", Cart);