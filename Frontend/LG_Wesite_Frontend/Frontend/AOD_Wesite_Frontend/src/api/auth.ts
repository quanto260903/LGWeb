import {jwtDecode} from 'jwt-decode';
import axios from 'axios';

// Helper function to decode the token and check expiration
function isTokenExpired(token: string) {
  const decoded = jwtDecode(token);
  const currentTime = Date.now() / 1000;
  return decoded.exp < currentTime;
}

// Fetch a new token from your API
export async function getToken() {

  const response = await axios.post(process.env.NEXT_PUBLIC_ENDPOINT_WEB_API + '/auth/token', {
    clientId: process.env.NEXT_PUBLIC_CLIENT_ID,
    clientSecret: process.env.NEXT_PUBLIC_CLIENT_SECRET
  });
  const token = await response.data.accessToken;
  return token;
}

// Function to fetch token and refresh if needed
export async function fetchWithAuth() {
  let token = localStorage.getItem('auth_token');

  if (!token || isTokenExpired(token)) {
    // Fetch a new token if not present or expired
    token = await getToken();
    localStorage.setItem('auth_token', token); // Store the new token
  }

  return token;
}
