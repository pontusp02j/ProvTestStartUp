import axios from 'axios';

/*

 Exampel how to use it 

 import { api } from './api'; // adjust the path as necessary

// Example usage in an async function
async function createItem() {
    try {
        const newItem = await api.create('items', { name: 'NewItem', price: 100 });
        console.log('Item created:', newItem);
    } catch (error) {
        console.error('Error creating item:', error);
    }
}

// You can similarly use api.read, api.update, and api.delete

*/

const API_BASE_URL = 'https://localhost:44420';

const apiClient = axios.create({
    baseURL: API_BASE_URL,
    headers: {
        'Content-Type': 'application/json',
        // Add any other headers here
    },
});

export const api = {
    create: async (path, data) => {
        try {
            const response = await apiClient.post(`/${path}`, data);
            return response.data;
        } catch (error) {
            console.error('Create error:', error);
            throw error;
        }
    },

    read: async (path) => {
        try {
            const response = await apiClient.get(`/${path}`);
            return response.data;
        } catch (error) {
            console.error('Read error:', error);
            throw error;
        }
    },

    update: async (path, data) => {
        try {
            const response = await apiClient.put(`/${path}`, data);
            return response.data;
        } catch (error) {
            console.error('Update error:', error);
            throw error;
        }
    },

    delete: async (path) => {
        try {
            const response = await apiClient.delete(`/${path}`);
            return response.data;
        } catch (error) {
            console.error('Delete error:', error);
            throw error;
        }
    }
};