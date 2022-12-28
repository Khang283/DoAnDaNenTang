const { parse } = require('dotenv');
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
                                Cart.findByIdAndUpdate(cartID,
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
}

module.exports = new CartController();
module.exports = new CartController();