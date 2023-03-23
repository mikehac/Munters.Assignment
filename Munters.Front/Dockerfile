#stage 1
FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install -g npm@9.5.0
RUN npm link @angular/cli
RUN ng build

#stage 2
FROM nginx:alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=node /app/dist/munters.front /usr/share/nginx/html
CMD ["nginx", "-g", "daemon off;"]