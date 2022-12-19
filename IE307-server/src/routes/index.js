const productsRoutes = require('./products');

function Routes(app){
    //PRODUCTS
    app.use('/products', productsRoutes);
}

module.exports=Routes;