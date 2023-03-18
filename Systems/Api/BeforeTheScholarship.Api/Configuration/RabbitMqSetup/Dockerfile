FROM rabbitmq
COPY ["../../../../Services/BeforeTheScholarship.Services.RabbitMqService/plugins/rabbitmq_delayed_message_exchange-3.11.1.ez", "plugins"]
WORKDIR /plugins
RUN rabbitmq-plugins enable rabbitmq_delayed_message_exchange