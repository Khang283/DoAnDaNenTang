const mongoose =require('mongoose');
const Schema=mongoose.Schema;
const ObjectId=Schema.ObjectId;
const mongooseDelete=require('mongoose-delete');

const Product = new Schema({
    productId: { type: ObjectId },
    name: { type: String, required: true, max: 255 },
    description: { type: String, max: 800 },
    image: { type: String, max: 255 },
    price: { type: String, default: 0 },
    type: { type: String, max: 25 },
    material: { type: String, max: 25, default: 'Silver' },
    sold: {type: Number, default: 0},
    createdAt: { type: Date, default: Date.now },
    updatedAt: { type: Date, default: Date.now },
});

Product.plugin(mongooseDelete,{overrideMethods: 'all'});

module.exports = mongoose.model('Product', Product);