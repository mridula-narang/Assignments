<template>
    <section>
        <h1>Products</h1>

        <!-- Dropdown for categories -->
        <div class="dropdown">
            <select v-model="selectedCategory" @change="filterByCategory">
                <option value="">Select Category</option>
                <option v-for="category in categories" :key="category" :value="category">{{ category }}</option>
            </select>
        </div>

        <div v-if="products.length > 0">
            <h2>List</h2>
            <!-- Product grid using flexbox -->
            <div class="product-grid">
                <div v-for="product in filteredProducts" :key="product.id" class="card">
                    <img class="card-img-top" :src="product.thumbnail" alt="Card image cap">
                    <div class="card-body">
                        <h5 class="card-title">{{ product.title }}</h5>
                        <p class="card-text">{{ product.description }}</p>
                        <button @click="btnClick(product.id)" class="btn btn-primary">Buy @ {{ product.price }}</button>
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>

<script>
export default {
    name: "ProductList",
    data() {
        return {
            products: [],
            categories: [],
            selectedCategory: "",
            filteredProducts: [],
            btnClick: (args) => {
                alert("Product added to cart " + args);
            }
        };
    },
    mounted() {
        console.log("Component loaded");
        fetch("https://dummyjson.com/products")
            .then((res) => res.json())
            .then((data) => {
                this.products = data.products;
                this.filteredProducts = this.products; 
                this.categories = [
                    ...new Set(this.products.map((product) => product.category))
                ];
            });
    },
    methods: {
        filterByCategory() {
            if (this.selectedCategory) {
                this.filteredProducts = this.products.filter(
                    (product) => product.category === this.selectedCategory
                );
            } else {
                this.filteredProducts = this.products;
            }
        }
    }
};
</script>

<style>
/* Styling for the card layout */
.product-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
    gap: 16px; /* Space between cards */
    margin-top: 20px;
}

.card {
    border: 1px solid #ddd;
    border-radius: 8px;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
    overflow: hidden;
    display: flex;
    flex-direction: column;
    padding: 16px;
}

.card-img-top {
    width: 100%;
    height: auto;
    object-fit: cover;
    border-bottom: 1px solid #ddd;
}

.card-body {
    padding-top: 8px;
}

/* Dropdown menu styling */
.dropdown select {
    padding: 8px;
    font-size: 16px;
    margin-bottom: 20px;
    border: 1px solid #ddd;
    border-radius: 4px;
}
</style>
