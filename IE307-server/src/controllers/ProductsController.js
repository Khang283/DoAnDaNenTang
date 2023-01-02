const Product=require('../models/Products');
const Account=require('../models/Account');
class ProductsController{

    //[GET] /products
    index(req,res){
        let random=Math.random()*10
        Product.find().skip(random).limit(8).lean().exec((err,products)=>{
            if(products){
                res.status(200).json(products);
            }
            else{
                res.status(404).send(err);
            }
        })
    }

    

    //[POST] /product/addToFavorite
    async AddToFavorite(req, res){
        const userID=req.body.userID;
        const productID=req.body.productID;
        let account = await Account.findById(userID).exec();
        let product = await Product.findById(productID).exec();
        if(product){
            if(account){
                account.update({$push: {favorite: {item: product}}},(err, tk)=>{
                    if(tk){
                        return res.status(200).send("Thêm vào yêu thích thành công");
                    }
                    else if(!tk){
                        return res.status(404).send("Không tìm thấy tài khoản");
                    }
                    else{
                        return res.status(500).send("Đã xảy ra lỗi");
                    }
                })
            }
            else{
                return res.status(404).send("Không tìm thấy tài khoản");
            }
        }
        else{
            return res.status(404).send("Không tìm thấy sản phẩm");
        }
    }

    //[POST] products/checkfavorite
    async CheckFavorite(req, res){
        const userID=req.body.userID;
        const productID=req.body.productID;
        let product= await Product.findById(productID).exec();
        //console.log(product);
        if(product){
            Account.findOne({_id: userID, favorite: {$elemMatch: {item: product}}},(err,account)=>{
                if(account){
                    //console.log(account);
                    return res.status(200).send("Sản phẩm đã có trong danh sách yêu thích");
                }
                else if(err){
                    return res.status(500).send("Đã xảy ra lỗi");
                }
                else{
                    return res.status(404).send("Sản phẩm chưa có trong giỏ hàng")
                }
            })
        }
        else{
            return res.status(400).send("Sản phẩm không tồn tại");
        }
    }

    //[PUT] products/removefavorite
    async RemoveFavorite(req, res){
        const userID=req.body.userID;
        const productID=req.body.productID;
        let product= await Product.findById(productID).exec();
        //console.log(product);
        if(product){
            Account.findOne({_id: userID, favorite: {$elemMatch: {item: product}}},(err,account)=>{
                if(account){
                    //console.log(account);
                    let itemID;
                    account.favorite.forEach(sp => {
                        if(sp.item.id==productID){
                            itemID=sp.id;
                        }
                    });
                    account.update({$pull: {favorite: {_id: itemID}}},(err,tk)=>{
                        if(tk){
                            return res.status(200).send("Sản phẩm đã bị loại khỏi danh sách yêu thích");
                        }
                        else{
                            return res.status(505).send("Lỗi 505");
                        }
                    })
                }
                else if(err){
                    return res.status(500).send("Đã xảy ra lỗi");
                }
                else{
                    return res.status(404).send("Sản phẩm chưa có trong giỏ hàng")
                }
            })
        }
        else{
            return res.status(400).send("Sản phẩm không tồn tại");
        }
    }

    //[GET] /products/?category/?page
    Category(req, res){
        const page=req.params.page || 1;
        const category=req.params.category;
        let limit=8;
        console.log(category, page);
        Product.find({type: category}).skip(limit*page-limit).limit(8).lean().exec((err,product)=>{
            if(product){
                return res.status(200).json(product);
            }
            else{
                return res.status(404).send(err.message);
            }
        });
    }
}

module.exports = new ProductsController();