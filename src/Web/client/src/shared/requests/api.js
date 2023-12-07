import axios from 'axios';

const API_BASE_URL = 'http://localhost:5248/api/';

const apiClient = axios.create({
    baseURL: API_BASE_URL,
    headers: {
        'Content-Type': 'application/json',
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