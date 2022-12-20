const mongoose = require('mongoose');
const Schema = mongoose.Schema;
const ObjectId=Schema.ObjectId;

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
    }

},{
    timestamps: true,
});

module.exports = mongoose.model("Account", Account);