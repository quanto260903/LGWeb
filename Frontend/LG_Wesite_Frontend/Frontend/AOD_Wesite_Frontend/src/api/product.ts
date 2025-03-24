import axios from 'axios';
import { fetchWithAuth } from './auth';

export async function GetResourcescheduling() {
  const token = await fetchWithAuth();
  
  try {
    const response = await axios.get(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/Product/resourcescheduling', {
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

export async function GetDigitalSignage() {
  const token = await fetchWithAuth();
  
  try {
    const response = await axios.get(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/Product/digitalsignage', {
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

export async function GetExchangeandOutlook() {
  const token = await fetchWithAuth();
  
  try {
    const response = await axios.get(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/Product/exchangeandoutlook', {
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

export async function GetProduct() {
  const token = await fetchWithAuth();
  
  try {
    const response = await axios.get(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/Product/positiontab', {
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