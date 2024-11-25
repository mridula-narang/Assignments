import axios from 'axios';

export const login = async (username, password) => {
  try {
    const response = await axios.post('http://localhost:5263/api/User/Login', { username, password });
    return response;
  } catch (error) {
    console.error('Error during login:', error);
    throw error;
  }
};