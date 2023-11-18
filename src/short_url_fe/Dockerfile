# Sử dụng một hình ảnh Node để xây dựng ứng dụng React
FROM node:18.18 AS build

# Đặt thư mục làm việc
WORKDIR /app

# Sao chép mã nguồn ứng dụng React vào thư mục làm việc
COPY . .

# Cài đặt các gói phụ thuộc và xây dựng ứng dụng
RUN npm install --force

RUN npm run build

# Đây là lần thứ hai của Dockerfile, sử dụng Nginx để phục vụ các tệp đã xây dựng
FROM nginx:1.23.3

# Sao chép tệp đã xây dựng từ giai đoạn trước vào thư mục /usr/share/nginx/html
COPY --from=build /app/build /usr/share/nginx/html
COPY --from=build /app/nginx/default.conf /etc/nginx/conf.d/default.conf

# Expose port 80
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]
