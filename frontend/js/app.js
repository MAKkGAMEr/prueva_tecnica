// Obtener todos los productos
function getAllProducts() {
    fetch('https://localhost:7021/api/Product')
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al cargar los productos');
            }
            return response.json();
        })
        .then(products => {
            if (products && Array.isArray(products)) {
                let productsList = '<ul>';
                products.forEach(product => {
                    productsList += `<li>ID: ${product.productoId}, Nombre: ${product.nombre}, Precio: ${product.precio}</li>`;
                });
                productsList += '</ul>';
                document.getElementById('allProductsContainer').innerHTML = productsList;
            } else {
                document.getElementById('allProductsContainer').innerHTML = 'No se encontraron productos.';
            }
        })
        .catch(error => {
            document.getElementById('allProductsContainer').innerHTML = 'Error al cargar los productos: ' + error.message;
        });
}

//Producto por ID
function getProductById() {
    const productId = document.getElementById('productIdSearch').value;
    if (!productId) {
        alert('Por favor ingrese un ID de producto');
        return;
    }

    fetch(`https://localhost:7021/api/Product/${productId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Producto no encontrado');
            }
            return response.json();
        })
        .then(product => {
            if (product) {
                document.getElementById('productByIdContainer').innerHTML = `
                    <p>ID: ${product.productoId}</p>
                    <p>Nombre: ${product.nombre}</p>
                    <p>Precio: ${product.precio}</p>
                `;
            } else {
                document.getElementById('productByIdContainer').innerHTML = 'Producto no encontrado';
            }
        })
        .catch(error => {
            document.getElementById('productByIdContainer').innerHTML = 'Error al cargar el producto: ' + error.message;
        });
}

// Crear producto
function createProduct() {
    const productId = document.getElementById('productId').value;
    const productName = document.getElementById('productName').value;
    const productPrice = document.getElementById('productPrice').value;

    if (!productId || !productName || !productPrice) {
        document.getElementById('responseMessage').innerHTML = 'Por favor complete todos los campos.';
        return;
    }

    const product = {
        ProductoId: parseInt(productId),
        nombre: productName,
        precio: parseFloat(productPrice)
    };

    fetch('https://localhost:7021/api/Product', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(product)
    })
    .then(response => {
        if (!response.ok) {
            return response.text().then(text => { throw new Error(text) }); 
        }
        return response.json();
    })
    .then(data => {
        document.getElementById('responseMessage').innerHTML = 'Producto creado correctamente.';
    })
    .catch(error => {
        document.getElementById('responseMessage').innerHTML = 'Error al crear el producto: ' + error.message;
    });
}

// Actualizar producto
function updateProduct() {
    const productId = document.getElementById('updateProductId').value;
    const productName = document.getElementById('updateProductName').value;
    const productPrice = document.getElementById('updateProductPrice').value;

    if (!productId || !productName || !productPrice) {
        document.getElementById('updateMessage').innerHTML = "Por favor ingrese todos los campos.";
        return;
    }

    const product = {
        ProductoId: parseInt(productId),
        nombre: productName,
        precio: parseFloat(productPrice),
        fechaCreacion: new Date().toISOString()
    };

    fetch(`https://localhost:7021/api/Product/${productId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(product)
    })
    .then(response => {
        if (!response.ok) {
            return response.text().then(text => { throw new Error(text) });
        }

        document.getElementById('updateMessage').innerHTML = 'Producto actualizado correctamente.';
    })
    .catch(error => {
        document.getElementById('updateMessage').innerHTML = 'Error al actualizar producto: ' + error.message;
    });
}

// Eliminar producto
function deleteProduct() {
    const productId = document.getElementById('deleteProductId').value;

    if (!productId) {
        document.getElementById('deleteMessage').innerHTML = "Por favor ingrese el ID del producto que desea eliminar.";
        return;
    }

    fetch(`https://localhost:7021/api/Product/${productId}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (response.ok) {
            document.getElementById('deleteMessage').innerHTML = 'Producto eliminado correctamente.';
        } else {
            return response.text().then(text => { throw new Error(text) });
        }
    })
    .catch(error => {
        document.getElementById('deleteMessage').innerHTML = 'Error al eliminar producto: ' + error.message;
    });
}
