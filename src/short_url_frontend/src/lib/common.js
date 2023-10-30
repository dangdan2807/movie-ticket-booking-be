export function storeTokenInLocalStorage(token) {
  localStorage.setItem('token', token);
}

export function getTokenFromLocalStorage() {
  return localStorage.getItem('token');
}

export function deleteTokenFromLocalStorage() {
  return localStorage.removeItem('token');
}
