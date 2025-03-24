import axios from 'axios';
import { fetchWithAuth } from './auth';

export async function GetAboutTop() {
  const token = await fetchWithAuth();
  
  try {
    const response = await axios.get(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/about/aboutus', {
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

export async function GetAboutStrategic() {
  const token = await fetchWithAuth();
  debugger;
  try {
    const response = await axios.get(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/about/strategic', {
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

export async function GetAboutCEO() {
  const token = await fetchWithAuth();
  
  try {
    const response = await axios.get(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/about/ceo', {
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



export async function GetAboutTeam() {
  const token = await fetchWithAuth();
  
  try {
    const response = await axios.get(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/about/team', {
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