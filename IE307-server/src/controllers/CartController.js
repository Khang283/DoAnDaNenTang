const { parse } = require('dotenv');
const Account = require('../models/Account');
const Cart=require('../models/Cart');
const Products = require('../models/Products');

class CartController{

    //[POST] cart/
    GetList(req, res){
        const userId=req.body.userId;
        console.log(userId);
        Cart.findOne({userID: userId},(err,cart)=>{
            if(!cart){
                return res.status(404).send("Không tìm thấy giỏ hàng");
            }
            else if(err){
                return res.status(500).send("Đã xảy ra lỗi");
            }
            else{
                return res.status(200).json(cart);
            }
        })
    }

    //[POST] cart/add
    add(req, res){
        const userID=req.body.userID;
        const productID=req.body.productID;
        Products.findById(productID,(err,product)=>{
            if(product){
                Cart.findOne({userID: userID},(err,cart)=>{
                    if(cart){
                        const cartID=cart.id;
                        const sp={
                            item: product,
                            quantity: 1,
                            price: product.price
                        };
                        const giaTien=parseInt(product.price);
                        console.log(cartID);
                        Cart.findOne({_id: cartID, items: {$elemMatch: {item: product}}},(err,newCart)=>{
                            console.log(newCart);
                            if(newCart){
                                Cart.findOneAndUpdate({_id: cartID, items: {$elemMatch: {item: product}}},{
                                    $inc: {
                                        totalPrice: giaTien,
                                        totalQuantity: 1,
                                        'items.$.price': giaTien,
                                        'items.$.quantity': 1
                                    }
                                },{new: true},(err,gioHang)=>{
                                    console.log(gioHang)
                                    if(gioHang){
                                        return res.status(200).send("Cập nhật sản phẩm thành công");
                                    }
                                    else{
                                        return res.status(500).send("Đã xảy ra lỗi");
                                    }
                                })
                            }
                            else if(!newCart){
                                Cart.findOneAndUpdate({_id: cartID},
                                    {$push: {items: sp}, 
                                     $inc: {
                                        totalPrice: giaTien,
                                        totalQuantity: 1,
                                    }},
                                    {upsert: true, new: true},
                                    (err,giohang)=>{
                                        console.log(giohang);
                                        if(giohang){
                                            return res.status(200).send("Đã thêm sản phẩm vào giỏ hàng");
                                        }
                                        else{
                                            return res.status(500).send("Đã xảy ra lỗi");
                                        }
                                    })
                            }
                            else{
                                return res.status(500).send("Đã xảy ra lỗi");
                            }
                        })
                    }
                    else if(!cart){
                        const cart = new Cart({
                            userID: userID,
                            items:[{
                                item: product,
                                quantity: 1,
                                price: product.price,
                            }],
                            totalQuantity: 1,
                            totalPrice: parseInt(product.price),
                        });
                        cart.save((err,cart)=>{
                            return res.status(201).send("Tạo giỏ hàng và thêm sản phẩm thành công");
                        });
                    }
                    else{
                        return res.status(500).send("Đã xảy ra lỗi");
                    }
                })
            }
            else{
                return res.status(404).send("Không tìm thấy sản phẩm");
            }
        })
    }

    //[DELETE] cart/delete
    async delete(req, res){
        const userID=req.body.userID;
        const productID=req.body.productID;
        const product=await Products.findById(productID);
        if(product){
            Cart.findOne({userID: userID},(err,cart)=>{
                if(cart){
                    const cartID=cart.id;
                    let itemID, itemQuanity;
                    cart.items.forEach(sp => {
                        if(sp.item.id==productID){
                            itemID=sp.id;
                            itemQuanity=sp.quantity;
                        }
                    });
                    Cart.findOneAndUpdate({_id: cartID, items: {$elemMatch: {item: product}}},
                        {$pull: {items: {_id: itemID }},
                        $inc: {totalPrice: -(product.price*itemQuanity), totalQuantity: -itemQuanity}}
                        ,(err,gioHang)=>{
                            if(gioHang){
                                return res.status(200).send("Đã xóa sản phẩm thành công");
                            }
                            else if(!gioHang){
                                return res.status(400).send("Không tìm thấy sản phẩm");
                            }
                            else{
                                return res.status(500).send("Lỗi server");
                            }
                        })
                }
                else if(!cart){
                    return res.status(404).send("Không tìm thấy giỏ hàng");
                }
                else{
                    return res.status(500).send("Lỗi server");
                }
            })
        }
        else{
            return res.status(500).send("Sản phẩm không tồn tại");
        }
    }

    //[PUT] cart/inc
    async inc(req,res){
        const userID=req.body.userID;
        const productID=req.body.productID;
        const product=await Products.findById(productID);
        if(product){
            Cart.findOne({userID: userID},(err,cart)=>{
                if(cart){
                    const cartID=cart.id;
                    let itemID, itemQuanity;
                    cart.items.forEach(sp => {
                        if(sp.item.id==productID){
                            itemID=sp.id;
                            itemQuanity=sp.quantity;
                        }
                    });
                    Cart.findOneAndUpdate({_id: cartID, items: {$elemMatch: {item: product}}},
                        {$inc: {totalPrice: product.price, totalQuantity: 1, 'items.$.quantity': 1, 'items.$.price': product.price}}
                        ,(err,gioHang)=>{
                            if(gioHang){
                                return res.status(200).send("Đã tăng số lượng sản phẩm thành công");
                            }
                            else if(!gioHang){
                                return res.status(400).send("Không tìm thấy sản phẩm");
                            }
                            else{
                                return res.status(500).send("Lỗi server");
                            }
                        })
                }
                else if(!cart){
                    return res.status(404).send("Không tìm thấy giỏ hàng");
                }
                else{
                    return res.status(500).send("Lỗi server");
                }
            })
        }
        else{
            return res.status(500).send("Sản phẩm không tồn tại");
        }
    }

    //[PUT] cart/reduce
    async reduce(req, res){
        const userID=req.body.userID;
        const productID=req.body.productID;
        const product=await Products.findById(productID);
        if(product){
            Cart.findOne({userID: userID},(err,cart)=>{
                if(cart){
                    const cartID=cart.id;
                    let itemID, itemQuanity;
                    cart.items.forEach(sp => {
                        if(sp.item.id==productID){
                            itemID=sp.id;
                            itemQuanity=sp.quantity;
                        }
                    });
                    if(itemQuanity==1){
                        return res.status(406).send("Không thể giảm tiếp sản phẩm");
                    }
                    Cart.findOneAndUpdate({_id: cartID, items: {$elemMatch: {item: product}}},
                        {$inc: {totalPrice: -product.price, totalQuantity: -1, 'items.$.quantity': -1, 'items.$.price': -product.price}}
                        ,(err,gioHang)=>{
                            if(gioHang){
                                return res.status(200).send("Đã giảm số lượng sản phẩm thành công");
                            }
                            else if(!gioHang){
                                return res.status(400).send("Không tìm thấy sản phẩm");
                            }
                            else{
                                return res.status(500).send("Lỗi server");
                            }
                        })
                }
                else if(!cart){
                    return res.status(404).send("Không tìm thấy giỏ hàng");
                }
                else{
                    return res.status(500).send("Lỗi server");
                }
            })
        }
        else{
            return res.status(500).send("Sản phẩm không tồn tại");
        }
    }

    //[POST] /cart/checkout
    async Checkout(req, res){
        const userID=req.body.userID;
        const receiver=req.body.receiver;
        const address=req.body.address;
        const phone=req.body.phone;
        const pay=req.body.pay;
        let account=await Account.findById(userID).exec();
        if(account){
            let cart=await Cart.findOne({userID: userID}).exec();
            if(cart){
                cart.items.forEach(sp => {
                    Products.findById(sp.item.id,(err, product)=>{
                        product.update({$inc: {sold: sp.quantity}}).exec(()=>{
                            console.log("Cập nhật sản phẩm thành công");
                        });
                    });
                });
                await cart.update({$set: {receiver: receiver, address: address, phone: phone, pay: pay}}).exec((err, giohang)=>{
                    if(giohang){
                        Cart.deleteById(cart.id,(err,gioHang)=>{
                            if(gioHang){
                                return res.status(200).send("Đã thanh toán thành công");
                            }
                            else if(!gioHang){
                                return res.status(404).send("Không tìm thấy giỏ hàng để xóa");
                            }
                            else{
                                return res.status(500).send("Lỗi server");
                            }
                        })
                    }
                    else{
                        return res.status(500).send("Không thể update giỏ hàng");
                    }
                })
            }
            else{
                return res.status(404).send("Không tìm thấy giỏ hàng");
            }
        }
        else{
            return res.status(404).send("Không tìm thấy tài khoản");
        }

    }

    //[POST] cart/history
    History(req, res){
        const userID=req.body.userID;
        Cart.findDeleted({userID: userID},(err, cart)=>{
            if(cart){
                return res.status(200).json(cart);
            }
            else if(!cart){
                return res.status(404).send("Không tìm thấy lịch sử");
            }
            else{
                return res.status(500).send("Lỗi server");
            }
        })
    }
}

module.exports = new CartController();