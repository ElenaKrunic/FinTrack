import axios from 'axios';

const API_URL = 'http://localhost:5194/api/auth/';

const register = async (payload) => {
  try {
    const response = await axios.post(
      `${API_URL}register`,
      payload,
      {
        withCredentials: true 
      }
    );
    return response.data;
  } catch (error) {
    console.error('Error during registration', error);
    throw error;
  }
};

export default {
  register
};
