const Product=require('../models/Products');

class ProductsController{
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
}

module.exports = new ProductsController();