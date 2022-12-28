const productsRoutes = require('./products');
const accountRoutes = require('./account');
const cartRoutes = require('./cart');

function Routes(app){
    //PRODUCTS
    app.use('/products', productsRoutes);

    //ACCOUNT
    app.use('/account', accountRoutes);

    //CART
    app.use('/cart', cartRoutes);
}

module.exports=Routes;