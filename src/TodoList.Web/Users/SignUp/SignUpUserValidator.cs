﻿using FastEndpoints;
using FluentValidation;

namespace TodoList.Web.Users.SignUp;

public class SignUpUserValidator : Validator<SignUpUserRequest>
{
    //Use this as a reference https://github.com/marteinn/The-Big-Username-Blocklist/blob/main/list.txt
    private static readonly string[] _restrictedWords =
    [
        ".git", ".htaccess", ".htpasswd", ".well_known", "400", "401", "403", "404", "405", "406", "407", "408",
        "409", "410", "411", "412", "413", "414", "415", "416", "417", "421", "422", "423", "424", "426", "428",
        "429", "431", "500", "501", "502", "503", "504", "505", "506", "507", "508", "509", "510", "511",
        "_domainkey", "about", "about_us", "abuse", "access", "account", "accounts", "ad", "add", "admin",
        "administration", "administrator", "ads", "ads.txt", "advertise", "advertising", "aes128_ctr", "aes128_gcm",
        "aes192_ctr", "aes256_ctr", "aes256_gcm", "affiliate", "affiliates", "ajax", "alert", "alerts", "alpha",
        "amp", "analytics", "api", "app", "app_ads.txt", "apps", "asc", "assets", "atom", "auth", "authentication",
        "authorize", "autoconfig", "autodiscover", "avatar", "backup", "banner", "banners", "bbs", "beta",
        "billing", "billings", "blog", "blogs", "board", "bookmark", "bookmarks", "broadcasthost", "business",
        "buy", "cache", "calendar", "campaign", "captcha", "careers", "cart", "cas", "categories", "category",
        "cdn", "cgi", "cgi_bin", "chacha20_poly1305", "change", "channel", "channels", "chart", "chat", "checkout",
        "clear", "client", "close", "cloud", "cms", "com", "comment", "comments", "community", "compare", "compose",
        "config", "connect", "contact", "contest", "cookies", "copy", "copyright", "count", "cp", "cpanel",
        "create", "crossdomain.xml", "css", "curve25519_sha256", "customer", "customers", "customize", "dashboard",
        "db", "deals", "debug", "delete", "desc", "destroy", "dev", "developer", "developers",
        "diffie_hellman_group_exchange_sha256", "diffie_hellman_group14_sha1", "disconnect", "discuss", "dns",
        "dns0", "dns1", "dns2", "dns3", "dns4", "docs", "documentation", "domain", "download", "downloads",
        "downvote", "draft", "drop", "ecdh_sha2_nistp256", "ecdh_sha2_nistp384", "ecdh_sha2_nistp521", "edit",
        "editor", "email", "enterprise", "error", "errors", "event", "events", "example", "exception", "exit",
        "explore", "export", "extensions", "false", "family", "faq", "faqs", "favicon.ico", "features", "feed",
        "feedback", "feeds", "file", "files", "filter", "follow", "follower", "followers", "following", "fonts",
        "forgot", "forgot_password", "forgotpassword", "form", "forms", "forum", "forums", "friend", "friends",
        "ftp", "get", "git", "go", "graphql", "group", "groups", "guest", "guidelines", "guides", "head", "header",
        "help", "hide", "hmac_sha", "hmac_sha1", "hmac_sha1_etm", "hmac_sha2_256", "hmac_sha2_256_etm",
        "hmac_sha2_512", "hmac_sha2_512_etm", "home", "host", "hosting", "hostmaster", "htpasswd", "http", "httpd",
        "https", "humans.txt", "icons", "images", "imap", "img", "import", "index", "info", "insert", "investors",
        "invitations", "invite", "invites", "invoice", "is", "isatap", "issues", "it", "jobs", "join", "js", "json",
        "keybase.txt", "learn", "legal", "license", "licensing", "like", "limit", "live", "load", "local",
        "localdomain", "localhost", "lock", "login", "logout", "lost_password", "m", "mail", "mail0", "mail1",
        "mail2", "mail3", "mail4", "mail5", "mail6", "mail7", "mail8", "mail9", "mailer_daemon", "mailerdaemon",
        "map", "marketing", "marketplace", "master", "me", "media", "member", "members", "message", "messages",
        "metrics", "mis", "mobile", "moderator", "modify", "more", "mx", "mx1", "my", "net", "network", "new",
        "news", "newsletter", "newsletters", "next", "nil", "no_reply", "nobody", "noc", "none", "noreply",
        "notification", "notifications", "ns", "ns0", "ns1", "ns2", "ns3", "ns4", "ns5", "ns6", "ns7", "ns8", "ns9",
        "null", "oauth", "oauth2", "offer", "offers", "online", "openid", "order", "orders", "overview", "owa",
        "owner", "page", "pages", "partners", "passwd", "password", "pay", "payment", "payments", "paypal", "photo",
        "photos", "pixel", "plans", "plugins", "policies", "policy", "pop", "pop3", "popular", "portal",
        "portfolio", "post", "postfix", "postmaster", "poweruser", "preferences", "premium", "press", "previous",
        "pricing", "print", "privacy", "privacy_policy", "private", "prod", "product", "production", "profile",
        "profiles", "project", "projects", "promo", "public", "purchase", "put", "quota", "redirect", "reduce",
        "refund", "refunds", "register", "registration", "remove", "replies", "reply", "report", "request",
        "request_password", "reset", "reset_password", "response", "return", "returns", "review", "reviews",
        "robots.txt", "root", "rootuser", "rsa_sha2_2", "rsa_sha2_512", "rss", "rules", "sales", "save", "script",
        "sdk", "search", "secure", "security", "select", "services", "session", "sessions", "settings", "setup",
        "share", "shift", "shop", "signin", "signup", "site", "sitemap", "sites", "smtp", "sort", "source", "sql",
        "ssh", "ssh_rsa", "ssl", "ssladmin", "ssladministrator", "sslwebmaster", "stage", "staging", "stat",
        "static", "statistics", "stats", "status", "store", "style", "styles", "stylesheet", "stylesheets",
        "subdomain", "subscribe", "sudo", "super", "superuser", "support", "survey", "sync", "sysadmin", "sysadmin",
        "system", "tablet", "tag", "tags", "team", "telnet", "terms", "terms_of_use", "test", "testimonials",
        "theme", "themes", "today", "tools", "topic", "topics", "tour", "training", "translate", "translations",
        "trending", "trial", "true", "umac_128", "umac_128_etm", "umac_64", "umac_64_etm", "undefined", "unfollow",
        "unlike", "unsubscribe", "update", "upgrade", "usenet", "user", "username", "users", "uucp", "var",
        "verify", "video", "view", "void", "vote", "vpn", "webmail", "webmaster", "website", "widget", "widgets",
        "wiki", "wpad", "write", "www", "www_data", "www1", "www2", "www3", "www4", "you", "yourname",
        "yourusername", "zlib"
    ];

    public SignUpUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(4, 20)
            .Matches("^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores.")
            .Must(NotContainRestrictedWords).WithMessage("Username contains restricted words.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
    }

    public static bool NotContainRestrictedWords(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return false;
        }

        return Array.TrueForAll(_restrictedWords,
            word => !username.Equals(word, StringComparison.CurrentCultureIgnoreCase));
    }
}
