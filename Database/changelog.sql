--liquibase formatted sql

--changeset mmic:create_table_user_roles
CREATE TABLE user_roles (
    id INT PRIMARY KEY AUTO_INCREMENT,
    code VARCHAR(50) UNIQUE NOT NULL,
    label VARCHAR(100) NOT NULL
);

--changeset mmic:create_table_refusal_reasons
CREATE TABLE refusal_reasons (
    id INT PRIMARY KEY AUTO_INCREMENT,
    code VARCHAR(50) UNIQUE NOT NULL,
    label VARCHAR(100) NOT NULL
);

--changeset mmic:create_table_event_status
CREATE TABLE event_status (
    id INT PRIMARY KEY AUTO_INCREMENT,
    code VARCHAR(50) UNIQUE NOT NULL,
    label VARCHAR(100) NOT NULL
);

--changeset mmic:create_table_users
CREATE TABLE users (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    name VARCHAR(100) NOT NULL,
    email VARCHAR(150) UNIQUE NOT NULL,
    password_hash TEXT NOT NULL,
    role_id INT NOT NULL,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    anonymized_at TIMESTAMP NULL,
    CONSTRAINT fk_users_role FOREIGN KEY (role_id) REFERENCES user_roles(id)
);

--changeset mmic:create_table_events
CREATE TABLE events (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    title VARCHAR(255) NOT NULL,
    description TEXT NOT NULL,
    date DATE NOT NULL,
    time TIME NOT NULL,
    location VARCHAR(255),
    latitude DOUBLE,
    longitude DOUBLE,
    category VARCHAR(50),

    organizer_id CHAR(36) NOT NULL,
    CONSTRAINT fk_events_organizer FOREIGN KEY (organizer_id) REFERENCES users(id),

    status_id INT NOT NULL,
    CONSTRAINT fk_events_status FOREIGN KEY (status_id) REFERENCES event_status(id),

    moderated_by CHAR(36) DEFAULT NULL,
    CONSTRAINT fk_events_moderated_by FOREIGN KEY (moderated_by) REFERENCES users(id),

    moderated_at TIMESTAMP NULL,
    refusal_reason_id INT DEFAULT NULL,
    CONSTRAINT fk_events_refusal_reason FOREIGN KEY (refusal_reason_id) REFERENCES refusal_reasons(id),

    refusal_comment TEXT,

    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

--changeset mmic:create_table_event_participants
CREATE TABLE event_participants (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    event_id CHAR(36),
    user_id CHAR(36),
    registered_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_participants_event FOREIGN KEY (event_id) REFERENCES events(id) ON DELETE CASCADE,
    CONSTRAINT fk_participants_user FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE
);

--changeset mmic:create_table_favorites
CREATE TABLE favorites (
    user_id CHAR(36),
    event_id CHAR(36),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id, event_id),
    CONSTRAINT fk_favorites_user FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    CONSTRAINT fk_favorites_event FOREIGN KEY (event_id) REFERENCES events(id) ON DELETE CASCADE
);

--changeset mmic:create_table_event_speakers
CREATE TABLE event_speakers (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    event_id CHAR(36),
    name VARCHAR(100) NOT NULL,
    bio TEXT,
    CONSTRAINT fk_speakers_event FOREIGN KEY (event_id) REFERENCES events(id) ON DELETE CASCADE
);

--changeset mmic:create_table_event_stats
CREATE TABLE event_stats (
    event_id CHAR(36) PRIMARY KEY,
    participant_count INT,
    category VARCHAR(50)
);

--changeset mmic:insert_default_user_roles
INSERT INTO user_roles (code, label) VALUES
('user', 'Utilisateur'),
('organizer', 'Organisateur'),
('admin', 'Administrateur');

--changeset mmic:insert_default_event_status
INSERT INTO event_status (code, label) VALUES
('en_attente', 'En attente'),
('valide', 'Validé'),
('refuse', 'Refusé'),
('publie', 'Publié');

--changeset mmic:insert_default_event_refusal_reasons
INSERT INTO refusal_reasons (code, label) VALUES
('incomplet', 'Incomplet'),
('inapproprie', 'Inapproprié'),
('doublon', 'Doublon'),
('autre', 'Autre');

--changeset mmic:create_table_events_images_to_store_s3_url
CREATE TABLE event_images (
    id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    event_id CHAR(36) NOT NULL,
    s3_url VARCHAR(500) NOT NULL,
    filename VARCHAR(255),
    description VARCHAR(255),
    uploaded_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (event_id) REFERENCES events(id) ON DELETE CASCADE
);
