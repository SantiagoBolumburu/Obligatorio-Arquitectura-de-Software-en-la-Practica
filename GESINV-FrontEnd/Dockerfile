# Stage 1
FROM node:19.8.1  as build-step
RUN mkdir -p /app
WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
RUN npm run build --prod


# Stage 2
FROM nginx:1.23.4-bullseye
COPY --from=build-step /app/dist/gesinv /usr/share/nginx/html
EXPOSE 80