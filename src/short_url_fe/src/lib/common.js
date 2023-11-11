import { toast } from 'react-toastify';

export function storeTokenInLocalStorage(token) {
  localStorage.setItem('token', token);
}

export function getTokenFromLocalStorage() {
  return localStorage.getItem('token');
}

export function deleteTokenFromLocalStorage() {
  return localStorage.removeItem('token');
}

export function handleError(response, messageError) {
  const resErr = response?.response?.data;
  const errorMessages = [];

  if (resErr?.errors !== undefined) {
    for (const field in resErr?.errors) {
      if (resErr?.errors.hasOwnProperty(field)) {
        errorMessages.push(resErr?.errors[field][0]);
      }
    }
    const resultString = errorMessages.join(', ');
    toast.error(resultString);
  } else if (resErr !== undefined) {
    toast.error(resErr?.message);
  } else {
    toast.error(messageError);
  }
}
