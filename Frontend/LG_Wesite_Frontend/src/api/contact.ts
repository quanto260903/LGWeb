import axios from 'axios';
import { fetchWithAuth } from './auth';

export async function getContactData() {
  const token = await fetchWithAuth();
  
  try {
    const response = await axios.get(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/contact/getContactTop', {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });
    return response.data;
  } catch (error) {
    if (error.response && error.response.status === 401) {
      // Token has expired or invalid, handle logout or token refresh
      console.log('Token expired or unauthorized.');
      // You could refresh token or redirect to login here
    }
  }
}


export async function getContactDataBottom() {
  const token = await fetchWithAuth();
  
  try {
    const response = await axios.get(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/contact/GetContactBottom', {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });
    return response.data;
  } catch (error) {
    if (error.response && error.response.status === 401) {
      // Token has expired or invalid, handle logout or token refresh
      console.log('Token expired or unauthorized.');
      // You could refresh token or redirect to login here
    }
  }
}
export async function PostContact(contactData: any) {
  const token = await fetchWithAuth();

  try {
    const response = await axios.post(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/contact/postContact', contactData, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

    return response.data;
  } catch (error) {
    if (error.response && error.response.status === 401) {
      // Token has expired or invalid, handle logout or token refresh
      console.log('Token expired or unauthorized.');
      // You could refresh token or redirect to login here
    }
    throw error; // Rethrow the error to handle it in the front-end
  }
}